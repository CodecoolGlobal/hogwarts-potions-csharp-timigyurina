import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";
import "../../App.css";

const StartBrewing = () => {
  const [creator, setCreator] = useState(1);
  const [potionName, setPotionName] = useState("");
  const [createdPotion, setCreatedPotion] = useState({});

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const [success, setSuccess] = useState();

  const navigate = useNavigate();
  const minPotionNameLength = 2;

  const isMinLength = (text, count) => {
    return text.trim().length >= count;
  };

  const startPotion = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    try {
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
      if (!response.ok) {
        //means I will have an error and it has a message property
        const error = responseData.message;
        clearInputs();
        setError(error);
        setIsLoading(false);
        console.log(error);
        return error;
      }
      clearInputs();
      setSuccess("You have successfully started a new Potion!");
      setIsLoading(false);
      console.log(responseData);
      setCreatedPotion(responseData);

      return responseData;
    } catch (err) {
      clearInputs();
      setIsLoading(false);
      console.log(err); 
      setError(err.message);
    }
  };

  const clearError = () => {
    setError(null);
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
        <MessageModal message={error} onClear={clearError} itIsAnError />
      ) : success ? (
        <MessageModal message={success} onClear={clearSuccess} buttonText="Go to list of Potions"/>
      ) : (
        <form className="start-potion-form" onSubmit={startPotion}>
          <TextField
            value={potionName}
            onChange={(e) => setPotionName(e.target.value)}
            className="potion-name"
            label="Potion's name"
            placeholder="Enter the name of your new Potion"
            error={!isMinLength(potionName, minPotionNameLength)}
            helperText={
              !isMinLength(potionName, minPotionNameLength) &&
              `Your Potion' name should be at least ${minPotionNameLength} characters long`
            }
            variant="standard"
          />

          <Button
            type="submit"
            className="start-potion-btn"
            disabled={!isMinLength(potionName, minPotionNameLength)}
            variant="contained"
          >
            Start new Potion!
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
