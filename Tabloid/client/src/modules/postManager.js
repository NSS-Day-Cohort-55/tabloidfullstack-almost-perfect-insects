import { getToken } from "./authManager";

const api = "/api/post";

export const getAllPosts = () => {
  return getToken().then((token) => {
    return fetch(api, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((res) => res.json());
  });
};

export const getPostById = (id) => {
  return getToken().then((token) =>
    fetch(api + `/${id}`, {
      headers: { Authorization: `Bearer ${token}` },
    }).then((r) => r.json()),
  );
};

export const addPost = (post) => {
  return getToken()
    .then((token) => fetch(api, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(post),
    })
      .then((resp) => resp.json())
    )
}

export const editPost = (post) => {
  return getToken()
  .then((token) => fetch(`${api}/${post.id}`, {
      method: "PUT",
      headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
      },
      body: JSON.stringify(post),
  }));
};

