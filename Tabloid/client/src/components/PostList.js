import React, {useEffect, useState} from "react";
import { getAllPosts } from "../modules/postManager";


const PostList = ( ) => {

    const [posts, setPosts] = useState([])

    const getPosts = () => {
        getAllPosts()
        .then(setPosts)
    }

    useEffect( () => {
        getPosts()
    },[])


    return (
        posts.map((post) => {
            return <p>
                {`${post.title} ${post.userProfile?.displayName} ${post.category?.name}`}
            </p>
        })
    )
}

export default PostList;