import React, { Component } from 'react';
import Zmanim from './Zmanim';
import Calendar from './Calendar';
import Schedule from './Schedule';
import AnnouncementItem from './AnnouncementItem';
import '../custom.css';
import DigitalClock from './DigitalClock';
import moment from 'moment';

import AwesomeSlider from 'react-awesome-slider';
import withAutoplay from 'react-awesome-slider/dist/autoplay';
import 'react-awesome-slider/dist/styles.css';

export class Home extends Component {
    static displayName = Home.name;

    timerHandle = null;

    constructor(props) {
        super(props);
        this.state = {
            zmanim: null, zmanimLoading: true,
            schedule: null, scheduleLoading: true,
            calendar: null, calendarLoading: true,
            announcements: null, announcementsLoading: true,
            afterShkia: false,
            queryDate: moment().format('yyyy-MM-DD'),
            tomorrowDate: moment().add(1, 'days').format('yyyy-MM-DD'),
            time: moment()
        };
    }

    AutoplaySlider = withAutoplay(AwesomeSlider);

    componentDidMount() {
        this.loadData();

        this.timerHandle = setInterval(
            () => {
                this.setState({ time: moment() });
                this.loadData();
            },
            60000
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerHandle);
    }

    loadData() {
        this.populateZmanimData();
        this.populateScheduleData();
        this.populateCalendarData();
        this.populateAnnouncementData();
    }

    renderAnnouncements(announcements) {
        return (
            <>
                {this.state?.announcementsLoading ? '' :
                    announcements.map(announcement =>
                        <div key={announcement.id}>
                            <AnnouncementItem key={announcement.id} name={announcement.name} description={announcement.description} />
                        </div>
                    )
                }
            </>
        )
    }

    render() {
        let contents = this.state?.announcementsLoading
            ? <p><em>Loading...</em></p>
            : this.renderAnnouncements(this.state?.announcements)


        return (
            <this.AutoplaySlider play={true}
                cancelOnInteraction={true}
                interval={15000} bullets={false} organicArrows={false} fillParent={true}>

                <div className='topContainer fullPage'>
                    <div className='topContaineritem rowContainer topRow'>
                    </div>
                    <div className='topContaineritem rowContainer middleRow'>
                        <div className='rowContainerItem'>
                            <Zmanim zmanim={this.state?.zmanim} loading={this.state.zmanimLoading} />
                        </div>
                        <div className='rowContainerItem columnContainer clockColumn'>
                            <div className='rowContainerItem' />
                            <div className='rowContainerItem'>
                                <DigitalClock time={this.state?.time} />
                            </div>
                        </div>
                        <div className='rowContainerItem'>
                            <Schedule schedule={this.state?.schedule} loading={this.state.scheduleLoading} />
                        </div>
                    </div>
                    <div className='topContaineritem rowContainer bottomRow'>
                        <div className='rowContainerItem'>
                            <Calendar calendar={this.state?.calendar} loading={this.state.calendarLoading} />
                        </div>
                    </div>

                </div>
            </this.AutoplaySlider>
        );
    }

    async populateZmanimData() {
        const response = await fetch('zmanim/' + this.state.queryDate);
        const data = await response.json();
        this.setState({
            zmanim: data.zmanim,
            zmanimLoading: false,
            afterShkia: (moment(data.zmanim.shkia) < moment() ? true : false),
            queryDate: this.state?.time.format('yyyy-MM-DD'),
            tomorrowDate: this.state?.time.add(1, 'days').format('yyyy-MM-DD')
        });
    }

    async populateCalendarData() {
        const response = await fetch('calendar/' + (this.state.afterShkia ? this.state.tomorrowDate : this.state.queryDate));
        const data = await response.json();
        this.setState({ calendar: data.calendar, calendarLoading: false });
    }

    async populateScheduleData() {
        const response = await fetch('schedulegroup/active/' + this.state.queryDate);
        const data = await response.json();
        this.setState({ schedule: data, scheduleLoading: false });
    }

    async populateAnnouncementData() {
        const response = await fetch('announcements/Active/' + this.state.queryDate);
        const data = await response.json();
        console.log(data);
        this.setState({ announcements: data, announcementsLoading: false });
    }

}

