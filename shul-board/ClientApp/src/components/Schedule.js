import React from 'react';
import TimeItem from './TimeItem';
import Slider from "react-slick";

import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

export default function Schedule({ schedule, loading }) {
    const renderSchedule = (schedule) => {
        console.log(schedule);
        const settings = {
            dots: false,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 3000,
            adaptiveHeight: true,
            arrows: false,
            pauseOnHover: false,
            initialSlide: 0,
            className: "slide",
            vertical: false
        };
        return (
            <div className='scheduleContainer panel schedulePanel'>
                <Slider {...settings}>
                    {schedule.map(group =>
                        <div key={group.id} className='scheduleitem'>
                            <div className='scheduleGroupName'>{group.name}</div>
                            {group.items.map(item =>
                                <TimeItem key={item.id} name={item.name} time={item.time} description={item.description} />
                            )}
                        </div>
                    )}
                </Slider>
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