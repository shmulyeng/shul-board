import React from 'react';
import TimeItem from './TimeItem';

export default function Schedule({ schedule, loading }) {
    const renderSchedule = (schedule) => {
        console.log(schedule);
        return (
            <div className='scheduleContainer panel schedulePanel'>
                {schedule.map(group =>
                    <div key={group.id} className='scheduleitem'>
                        <div className='scheduleGroupName'>{group.name}</div>
                        {group.items.map(item =>
                            <TimeItem key={item.id} name={item.name} time={item.time} description={item.description} />
                        )}
                    </div>
                )}

            </div>
        );
    }

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderSchedule(schedule);

    return (
        <>
            {contents}
        </>
    );
}