import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

import RecipeDetails from "../RecipeElements/RecipeDetails";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";

import "../../App.css";
import RenameRecipeForm from "../FormElements/RenameRecipeForm";

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
          {recipe && <RecipeDetails recipe={recipe} />}
          {isEditing && <RenameRecipeForm />}
        </div>
      )}
    </>
  );
};

export default RenameRecipe;
