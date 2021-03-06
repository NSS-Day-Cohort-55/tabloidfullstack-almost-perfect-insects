import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import PostList from "./PostList";
import PostDetails from "./PostDetails";
import PostForm from "./PostForm";
import TagList from "./TagList";
import CategoryList from "./CategoryList";
import { PostEdit } from "./PostEditForm";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <main>
      <Routes>
        <Route path="/">
          <Route
            index
            element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
          />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="post">
            <Route index element={<PostList />} />
          <Route path=":postId/Edit" element={<PostEdit/>}/>
            <Route path=":id" element={<PostDetails />} />
            <Route path="add" element={<PostForm />} />
          </Route>
          <Route path="tag">
            <Route index element={<TagList />} />
          </Route>
          <Route path="category">
            <Route index element={<CategoryList />} />
          </Route>
          <Route path="*" element={<p>Whoops, nothing here...</p>} />
        </Route>
      </Routes>

    </main>
  );
};
