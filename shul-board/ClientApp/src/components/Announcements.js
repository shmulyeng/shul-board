import React from 'react';
import AnnouncementItem from './AnnouncementItem';


export default function Announcements({ announcements, loading }) {

    const renderAnnouncements = (announcements) => {
        return (
            <>
                {announcements.map(announcement =>
                    <AnnouncementItem key={announcement.id} name={announcement.name} description={announcement.description} />
                )}

            </>
        );
    }

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderAnnouncements(announcements);

    return (
        <>
            {contents}
        </>
    );
}