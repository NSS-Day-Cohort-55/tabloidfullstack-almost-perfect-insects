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
