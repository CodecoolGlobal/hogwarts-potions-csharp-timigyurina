import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

import RecipeDetails from "../RecipeElements/RecipeDetails";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";
import RenameRecipeForm from "../FormElements/RenameRecipeForm";
import Button from "@mui/material/Button";

import "../../App.css";

const RenameRecipe = () => {
  const recipeId = useParams().recipeId;
  const navigate = useNavigate();
  const [recipe, setRecipe] = useState(null);
  const [isEditing, setIsEditing] = useState(false);

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const [success, setSuccess] = useState();

  const fetchRecipe = async () => {
    const url = `https://localhost:44390/api/recipes/details/${recipeId}`;
    setIsLoading(true);

    try {
      const response = await fetch(url);
      const responseData = await response.json();
      console.log(responseData);
      setIsLoading(false);

      if (!response.ok) {
        const error = response.message;
        setError(error);
        console.log(error);
        return;
      }

      setRecipe(responseData);
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  useEffect(() => {
    fetchRecipe();
  }, []);

  const clearSuccess = () => {
    setSuccess(null);
    navigate(`/recipes`, { replace: true });
  };

  const closeModal = () => {
    setError(null);
    setSuccess(null);
  };

  const toggleEdit = () => {
    setIsEditing(!isEditing);
  };

  const recipeWasRenamed = (responseData, errorMessage) => {
    !errorMessage &&
      setSuccess(
        `You have successfully renamed the Recipe to ${responseData.name}`
      );
    errorMessage && setError(errorMessage);

    setIsEditing(false);
    fetchRecipe();
  };

  return (
    <>
      {isLoading ? (
        <div className="center">
          <LoadingSpinner asOverlay />
        </div>
      ) : error ? (
        <MessageModal
          message={error}
          onClose={closeModal}
          onGoTo={clearSuccess}
          itIsAnError
        />
      ) : success ? (
        <MessageModal
          message={success}
          onClose={closeModal}
          onGoTo={clearSuccess}
          buttonText="Go back to Recipes"
        />
      ) : (
        <div className="rename-recipe-page">
          {recipe && (
            <>
              <RecipeDetails recipe={recipe} />
              <Button
                onClick={toggleEdit}
                className="form-btn"
                variant="contained"
                color="warning"
                sx={{margin: "1em"}}
              >
                Edit Recipe
              </Button>
            </>
          )}
          {isEditing && <RenameRecipeForm onUpdate={recipeWasRenamed} />}
        </div>
      )}
    </>
  );
};

export default RenameRecipe;
