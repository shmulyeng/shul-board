import React from 'react';
//import moment from 'moment'

export default function Calendar({ calendar, loading }) {
    const renderCalendar = (calendar) => {
        return (
            <div className='panel'>
                <div className='bottomRowItem'>
                    {calendar.hebrewDate} - {calendar.daf} - {calendar.parsha} {calendar.yomTov? ' - ' + calendar.yomTov:''}
                </div>
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
