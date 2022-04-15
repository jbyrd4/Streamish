import React from "react";
import { Switch, Route } from "react-router-dom";
import VideoList from "./VideoList";
import VideoForm from "./VideoForm";
import VideoDetails from "./VideoDetails";
import UserVideos from "./UserVideos";

function ApplicationViews () {
  return (
    <Switch>
      <Route path="/" exact>
        <VideoList />
      </Route>

      <Route path="/videos/add">
        <VideoForm />
      </Route>

      <Route path="/userprofile/:id">
        <UserVideos />
      </Route>

      <Route path="/videos/:id">{
          <VideoDetails />
      }</Route>
    </Switch>
  );
};

export default ApplicationViews;