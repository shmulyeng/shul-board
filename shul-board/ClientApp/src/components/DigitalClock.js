import React from 'react';
import moment from 'moment'

export default function DigitalClock({ time }) {

    const renderClock = (time) => {
        return (
            <div className='panel'>
                <h1 className='digitalClock'>
                    {(moment(time).format('ss') % 2 === 0 ? moment(time).format('h:mm a') : moment(time).format('h:mm a'))}
                </h1>
            </div>
        );
    }

    let contents = renderClock(time);

    return (
        <div >
            {contents}
        </div>
    );

};
