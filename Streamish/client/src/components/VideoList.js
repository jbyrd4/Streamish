import React, { useEffect, useReducer, useState } from "react";
import Video from "./Video";
import { getAllVideos, searchVideos } from "../modules/videoManager";

function VideoList () {
  const [videos, setVideos] = useState([]);
  const [search, setSearch] = useState("");

  const getVideos = () => {
    getAllVideos().then((videos) => setVideos(videos));
  };

  const collectSearchVideos = (q, bool) => {
      searchVideos(q, bool).then((videos) => setVideos(videos))
    };

  useEffect(() => {
    getVideos();
  }, []);

  return (
    <div>
      <div>
        <label htmlFor="searchBar">Search Videos </label>
        <input
          type="search"
          name="searchBar"
          id="searchBar"
          onChange={(e) => setSearch(e.target.value)}
        ></input>
        <button type="submit" onClick={() => collectSearchVideos(search, true)}>
          Search
        </button>
      </div>
      <div className="container">
        <div className="row justify-content-center">
          {videos.map((video) => (
            <Video video={video} key={video.id} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default VideoList;