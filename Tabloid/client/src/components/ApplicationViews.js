import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import PostList from "./PostList";
import PostDetails from "./PostDetails";
import PostForm from "./PostForm";
import CategoryList from "./CategoryList";

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
            <Route path=":id" element={<PostDetails />} />
            <Route path="add" element={<PostForm />} />
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
