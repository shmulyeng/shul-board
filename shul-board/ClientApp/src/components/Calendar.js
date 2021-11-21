import React from 'react';
//import moment from 'moment'

export default function Calendar({ calendar, loading }) {
    const renderCalendar = (calendar) => {
        return (
            <div className='panel'>
                <h1>
                    {calendar.hebrewDate} - {calendar.daf} - {calendar.parsha} {calendar.yomTov? ' - ' + calendar.yomTov:''}
                </h1>
            </div>
        );
    }

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderCalendar(calendar);

    return (
        <div>
            {contents}
        </div>
    );

};
