import React, { useEffect, useState } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import { useParams } from "react-router-dom";
import { getVideosByUser } from "../modules/userManager";
import Video from "./Video";

const UserVideos = () => {
  const [user, setUser] = useState({});
  const { id } = useParams();

  useEffect(() => {
    getVideosByUser(id).then(setUser);
  }, []);

  if (!user) {
    return null;
  }
  console.log(user);

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-sm-12 col-lg-6">
          <p>{user.name}</p>
          <ListGroup>
            {user.videos?.map((v) => (
              <ListGroupItem><Video video={v} /></ListGroupItem>
            ))}
          </ListGroup>
        </div>
      </div>
    </div>
  );
};

export default UserVideos;
