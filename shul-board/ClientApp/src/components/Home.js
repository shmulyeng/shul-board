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
import Slider from "react-slick";

import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

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
            queryDate: moment().format('yyyy-MM-DD HH:mm:ss'),
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
                            <AnnouncementItem key={announcement.id} announcement={announcement} />
                        </div>
                    )
                }
            </>
        )
    }

    render() {
        const settings = {
            dots: false,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 10000,
            adaptiveHeight: false,
            arrows: false,
            pauseOnHover: false,
            initialSlide: 0,
            className: "slide"
        };

        let contents = this.state?.announcementsLoading
            ? <p><em>Loading...</em></p>
            : this.renderAnnouncements(this.state?.announcements)

        console.log(contents);
        return (
            <Slider {...settings}>
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
                {this.state?.announcementsLoading ? '' :
                    this.state?.announcements.map(announcement =>
                        <div key={announcement.id} className="announcementSlide">
                            <AnnouncementItem key={announcement.id} announcement={announcement} />
                        </div>
                    )
                }
                {/*<Announcements announcements={this.state?.announcements} loading={this.state.announcementsLoading} />*/}
            </Slider>
        );
    }

    async populateZmanimData() {
        const response = await fetch('api/zmanim/' + this.state.queryDate);
        const data = await response.json();
        this.setState({
            zmanim: data.zmanim,
            zmanimLoading: false,
            afterShkia: (moment(data.zmanim.shkia) < moment() ? true : false),
            queryDate: this.state?.time.format('yyyy-MM-DD HH:mm:ss'),
            tomorrowDate: this.state?.time.add(1, 'days').format('yyyy-MM-DD')
        });
    }

    async populateCalendarData() {
        const response = await fetch('api/calendar/' + (this.state.afterShkia ? this.state.tomorrowDate : this.state.queryDate));
        const data = await response.json();
        this.setState({ calendar: data.calendar, calendarLoading: false });
    }

    async populateScheduleData() {
        const response = await fetch('api/schedulegroup/active/' + this.state.queryDate);
        const data = await response.json();
        this.setState({ schedule: data, scheduleLoading: false });
    }

    async populateAnnouncementData() {
        const response = await fetch('api/announcements/Active/' + this.state.queryDate);
        const data = await response.json();
        console.log(data);
        this.setState({ announcements: data, announcementsLoading: false });
    }

}

