import React, { useState, useEffect } from "react";
import { getPostById } from "../modules/postManager";
import { useParams } from "react-router-dom";
export default function PostDetails() {
  const [post, setPost] = useState(null);
  const { id } = useParams();

  const getPost = (id) => {
    getPostById(id).then(setPost);
  };

  useEffect(() => {
    getPost(id);
  });

  if (!post) {
    return null;
  }

  return (
    <div>
      <h4>{post.title}</h4>
      <h5>{post.userProfile.displayName}</h5>
      <h6>{post.publishDateTime}</h6>
      <img src={post.imageLocation} alt="none" />
      <p>{post.content}</p>
    </div>
  );
}
