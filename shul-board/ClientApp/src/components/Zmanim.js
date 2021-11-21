import React from 'react';
import moment from 'moment'
import TimeItem from './TimeItem';

export default function Zmanim({ zmanim, loading }) {
    const renderZmanim = (zmanim) => {
        return (
            <div className='scheduleContainer panel zmanimPanel'>
                <div className='scheduleitem'>
                    <div className='scheduleGroupName'>זמנים</div>
                    <TimeItem name='עלות השחר' time={zmanim.alos} showUpcoming='true' />
                    <TimeItem name='הנץ החמה' time={zmanim.neitz} showUpcoming='true' />
                    <TimeItem name='ס"ז קר"ש מג"א' time={zmanim.sofZmanShmaMGA} showUpcoming='true' />
                    <TimeItem name='ס"ז קר"ש גר"א' time={zmanim.sofZmanShmaGRA} showUpcoming='true' />
                    <TimeItem name='סוף זמן תפילה מג"א' time={zmanim.sofZmanTefilaMGA} showUpcoming='true' />
                    <TimeItem name='סוף זמן תפילה גר"א' time={zmanim.sofZmanTefilaGRA} showUpcoming='true' />
                    <TimeItem name='חצות' time={zmanim.chatzos} showUpcoming='true' />
                    {moment(zmanim.candleLighting, "YYYY-MM-DDTHH:mm:ss").day() > 4 ? <TimeItem name='הדלקת נרות' time={zmanim.candleLighting} showUpcoming='true' /> : ""}
                    <TimeItem name='שקיעה' time={zmanim.shkia} showUpcoming='true' />
                    <TimeItem name='צאת הכוכבים' time={zmanim.tzeis60} showUpcoming='true' />
                    <TimeItem name='צאת הכוכבים לר"ת' time={zmanim.tzeis72} showUpcoming='true' />
                </div >
            </div >

        );
    }

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderZmanim(zmanim);


    return (
        <>
            {contents}
        </>
    );
};

