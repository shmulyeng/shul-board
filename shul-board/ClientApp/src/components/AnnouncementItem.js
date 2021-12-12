import React from 'react';
import parse from 'html-react-parser';

export default function AnnouncementItem({ name, description }) {
    return (
        <div className="announcementSlideOuter">
            <div className="announcementSlideInner">
                {parse(description ?? '')}
            </div>
        </div>
    );
}