import React from 'react';
import parse from 'html-react-parser';

export default function AnnouncementItem({ name, description }) {
    return (
        <>
            {parse(description ?? '')}
        </>
    );
}