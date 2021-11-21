import React from 'react';
import moment from 'moment'
import parse from 'html-react-parser';

export default function TimeItem({ name, time, description, showUpcoming }) {
    const soon = moment(new Date()).add(15, 'minutes') > moment(time, "YYYY-MM-DDTHH:mm:ss") && moment(time, "YYYY-MM-DDTHH:mm:ss") > moment(new Date()) ? true : false;
    return (
        <>
            <div className='scheduleItemNameRow rtl'>
                <div className='scheduleItemNameRowItem'>
                    {name}
                </div>
                <div className='scheduleItemNameRowItem dottedLine'>
                    <div className='dottenLineInner'></div>
                </div>
                <div className={'scheduleItemNameRowItem ltr ' + (soon && showUpcoming ? "timeSoon" : "")}>
                    {moment(time, "YYYY-MM-DDTHH:mm:ss").format('h:mm')}
                </div>
            </div>
            <div className='scheduleItemDescription'>
                {parse(description ?? '')}
            </div>
        </>
    );
}