import React, { useState, useContext } from "react";
import { useNavigate, useParams } from "react-router-dom";

import LoadingSpinner from "../UIElements/LoadingSpinner";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Typography from "@mui/material/Typography";
import "../../App.css";

const RenameRecipeForm = ({ onUpdate }) => {
  const recipeId = useParams().recipeId;
  const [newName, setNewName] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const minRecipeNameLength = 2;

  const isMinLength = (text, count) => {
    return text.trim().length >= count;
  };

  const renameRecipe = async (e) => {
    e.preventDefault();

    try {
      setIsLoading(true);
      const response = await fetch(
        `https://localhost:44390/api/recipes/${recipeId}`,
        {
          method: "PUT",
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify({
            name: newName,
          }),
        }
      );
      const responseData = await response.json();
      console.log(responseData);
      setIsLoading(false);

      if (!response.ok) {
        const error = responseData.message;
        console.log(error);
        onUpdate(false, error); // In AddINgredient the params are: ingredient, responseData, errorMessage
        return error;
      }

      onUpdate(responseData, false); // if the response is ok, the rsponseData will be the Potion
    } catch (err) {
      setIsLoading(false);
      console.log(err);

      onUpdate(false, err.message);
    }
  };

  return (
    <>
      {isLoading ? (
        <div className="center">
          <LoadingSpinner asOverlay />
        </div>
      ) : (
        <>
          <Typography variant="h5" component="div">
            Rename the Recipe
          </Typography>
          <form onSubmit={renameRecipe}>
            <FormControl fullWidth>
              <TextField
                value={newName}
                onChange={(e) => setNewName(e.target.value)}
                className="recipe-name"
                label="Recipe's name"
                placeholder="Enter the new name of the Recipe"
                error={!isMinLength(newName, minRecipeNameLength)}
                helperText={
                  !isMinLength(newName, minRecipeNameLength) &&
                  `The name of the Recipe should be at least ${minRecipeNameLength} characters long`
                }
                variant="standard"
              />

              <Button
                type="submit"
                className="form-btn"
                disabled={!isMinLength(newName, minRecipeNameLength)}
                variant="contained"
              >
                Rename Recipe
              </Button>
            </FormControl>
          </form>
        </>
      )}
    </>
  );
};

export default RenameRecipeForm;
