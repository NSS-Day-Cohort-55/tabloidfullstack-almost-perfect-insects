import React from "react";
import { Link } from "react-router-dom";

const PostCard = ({ post }) => {
  return (
    <div>
      <h4>{post.title}</h4>
      <h5>{post.userProfile?.displayName}</h5>
      <p>{post.category.name}</p>
      <Link to={`/post/${post.id}`}>Details</Link>
      <Link to={`${post.id}/Edit`}>Edit</Link>
    </div>
  );
};
export default PostCard