using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Data.Base;
using shul_board.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Controllers
{

    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        protected BaseService<TEntity> service;

        public BaseEntityController(BaseService<TEntity> service)
        {
            this.service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            return Ok((await this.service.GetAllAsync(false)).ToList());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetAsync(int id)
        {
            var result = await service.GetAsync(id, false);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exists = await service.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            await service.DeleteAsync(id);
            return Ok();
        }

         [HttpPost]
        public async Task<ActionResult<TEntity>> PostAsync(TEntity entity)
        {
            var created = await service.CreateAsync(entity);
            return Ok(created);
        }

        [HttpPost("{id}")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> UpdateAsync(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            var exists = await service.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            try
            {
                var modified = await service.UpdateAsync(id, entity);
                return Ok(modified);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
