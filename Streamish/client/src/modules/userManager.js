const baseUrl = "/api/UserProfile";

export const getUser = (id) => {
  return fetch(`${baseUrl}/${id}`).then((res) => res.json());
};

export const getVideosByUser = (id) => {
    return fetch(`${baseUrl}/GetVideosByAuthor/${id}`).then((res) => res.json());
}