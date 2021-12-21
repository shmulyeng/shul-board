import React from 'react';
import parse from 'html-react-parser';

export default function AnnouncementItem({ announcement } ) {
    const divStyleInner = {
    };

    const divStyleOuter = {
        backgroundRepeat: "no-repeat",
        backgroundPosition: "center"
    };

    if (announcement.left || announcement.top) {
        divStyleInner.position = "absolute";
    }

    if (announcement.left) {
        divStyleInner.left = announcement.left + "px";
    }

    if (announcement.top) {
        divStyleInner.top = announcement.top + "px";
    }

    if (announcement.width) {
        divStyleInner.width = announcement.width + "px";
    }

    if (announcement.height) {
        divStyleInner.height = announcement.height + "px";
    }

    if (announcement.backgroundImage) {
        divStyleOuter.backgroundImage = `url(${announcement.backgroundImage})`;
    }

    return (
        <div className="announcementSlideOuter" style={divStyleOuter}>
            <div className="announcementSlideInner" style={divStyleInner}>
                {parse(announcement.description ?? '')}
            </div>
        </div>
    );
}