import { getToken } from "./authManager"

const baseUrl = "/api/category"

export const getCategories = () => {
    return getToken().then((token) =>
        fetch(baseUrl, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
        }).then((resp) => resp.json())
    )
}
