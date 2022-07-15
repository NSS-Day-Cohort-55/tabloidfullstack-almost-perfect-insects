import { getToken } from "./authManager"

const baseUrl = "/api/tag";

export const getAllTags = () => {
    return getToken().then((token) =>
        fetch(baseUrl, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => resp.json())
    )
}