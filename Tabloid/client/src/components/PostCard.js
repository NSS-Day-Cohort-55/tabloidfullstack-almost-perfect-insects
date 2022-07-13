import React from "react";

const PostCard = ({post}) => {
    return (
        <div> 
            <h4>
                {post.title}
            </h4>
            <h5>
                {post.userProfile?.displayName}
            </h5>
            <p>
                {post.category.name}
            </p>
        </div>
    )
}
export default PostCard