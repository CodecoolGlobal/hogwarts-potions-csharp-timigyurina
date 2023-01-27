import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import Button from "@mui/material/Button";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";
import ScienceIcon from '@mui/icons-material/Science';
import "../../App.css";

const StartBrewing = () => {
  const [creator, setCreator] = useState(1);
  const [potionName, setPotionName] = useState("");
  const [createdPotion, setCreatedPotion] = useState({});

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const [success, setSuccess] = useState();

  const navigate = useNavigate();

  const startPotion = async (e) => {
    e.preventDefault();
    
    try {
      setIsLoading(true);
      const response = await fetch(
        `https://localhost:44390/api/potions/brew/${creator}`,
        {
          method: "POST",
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify({
            potionName: potionName,
          }),
        }
      );
      const responseData = await response.json();
      console.log(responseData);

      clearInputs();
      setIsLoading(false);

      if (!response.ok) {
        //means I will have an error and it has an message property (I set this on the backend)
        const error = responseData.message;
        setError(error);
        console.log(error);
        return error;
      }

      setSuccess("You have successfully started a new Potion!");
      setCreatedPotion(responseData);
      return responseData;

    } catch (err) {
      clearInputs();
      setIsLoading(false);
      setError(err.message);
      console.log(err); 
    }
  };

  const closeModal = () => {
    setError(null);
    setSuccess(null);
  };
  
  const clearSuccess = () => {
    setSuccess(null);
    navigate("/potions", { replace: true });
  };

  const clearInputs = () => {
    setPotionName("");
    setCreatedPotion({});
  };

  return (
    <>
      {isLoading ? (
        <div className="center">
          <LoadingSpinner asOverlay />
        </div>
      ) : error ? (
        <MessageModal message={error} onClose={closeModal} itIsAnError />
      ) : success ? (
        <MessageModal message={success} onClose={closeModal} onGoTo={clearSuccess} buttonText="Go to list of Potions"/>
      ) : (
        <form className="start-potion-form" onSubmit={startPotion}>

          <Button
            type="submit"
            className="start-potion-btn"
            // disabled={!isMinLength(potionName, minPotionNameLength)}
            variant="contained"
            color="warning"
          >
            Start brewing <ScienceIcon />
          </Button>
        </form>
      )}
    </>
  );
};

export default StartBrewing;

/*  Form: 
  display success msg and link to the addingredient page for the potion 
  get the studentId from auth context 
*/
