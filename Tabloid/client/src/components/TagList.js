import React, { useEffect, useState } from "react";
import { getAllTags } from "../modules/tagManager";



const TagList = () => {

    const [tags, setTags] = useState([])

    const getTags = () => {
        getAllTags()
            .then(setTags)
    };

    useEffect(() => {
        getTags();
    }, []);

    // returns a page that lists all the Tag names ordered alphabetically (see server api/side)
    return (
        <div>

            {tags.map((tag) => (
                <li key={tag.id}>{tag.name}</li>
            ))}

        </div>
    )
}



export default TagList;