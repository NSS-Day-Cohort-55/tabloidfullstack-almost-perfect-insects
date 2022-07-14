import React, {useEffect, useState} from "react";
import { getAllPosts } from "../modules/postManager";
import PostCard from "./PostCard";
import { Link } from "react-router-dom";


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
        <div>
            <div>
                <Link to="/post/add">Add a post</Link>
            </div>
            {posts.map((post) => (
                <PostCard post={post} key={post.id} />
            ))}
        </div>
    )
}



export default PostList;