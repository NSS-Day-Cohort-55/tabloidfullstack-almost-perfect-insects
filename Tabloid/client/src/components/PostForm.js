import React, { useEffect, useState } from "react";
import { addPost } from "../modules/postManager";
import { useNavigate } from "react-router-dom";
import { getCategories } from "../modules/categoryManager";


const PostForm = () => {
  const [post, setPost] = useState({})
  const [categories, setCategories] = useState([])
  const navigate = useNavigate();

  const addNewPost = (event) => {
    event.preventDefault();
    addPost(post).then(() => navigate(`/post`))
  }

  const handleFormInput = (event) => {
    const newPost = {...post}
    const inputValue = event.target.value
    const inputId = event.target.id
    newPost[inputId] = inputValue
    setPost(newPost)
  }

    const handleSelectInput = (event) => {
        const newPost = { ...post }
        const inputValue = parseInt(event.target.value)
        newPost.categoryId = inputValue
        setPost(newPost)
    }

  useEffect(() => {
    getCategories().then(setCategories)
    }, []
  )

  return (
      <>
          <label htmlFor="title">Post Title:</label>
          <input type="text" id="title" onChange={handleFormInput} />
          <label htmlFor="content">Post Content:</label>
          <input type="text" id="content" onChange={handleFormInput} />
          <label htmlFor="imageLocation">Image Location:</label>
          <input type="text" id="imageLocation" onChange={handleFormInput} />
          <label htmlFor="publishDateTime">
              When would you like to publish your post?
          </label>
          <input type="date" id="publishDateTime" onChange={handleFormInput} />
          <label htmlFor="category">Select a category:</label>
          <select
              id="category"
              onChange={handleSelectInput}
              defaultValue="default"
          >
              <option value="default" disabled hidden>
                  Select One
              </option>
              {categories.map((category) => (
                  <option value={category.id} key={category.id}>
                      {category.name}
                  </option>
              ))}
          </select>
          <button type="submit" onClick={addNewPost}>
              Add Post
          </button>
      </>
  )

  
}

export default PostForm;