import { useState, useEffect } from "react";

import RecipesTable from "../RecipeElements/RecipesTable";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";

const Recipes = () => {
  const [recipes, setRecipes] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();

  const fetchRecipes = async () => {
    const url = "https://localhost:44390/api/recipes";
    
    try {
      setIsLoading(true);
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

      setRecipes(responseData);
      
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  useEffect(() => {
    fetchRecipes();
  }, []);

  const clearError = () => {
    setError(null);
  };

  return (
    <>
      {isLoading ? (
        <div className="center">
          <LoadingSpinner asOverlay />
        </div>
      ) : error ? (
        <MessageModal message={error} onClose={clearError} itIsAnError />
      ) : (
        <RecipesTable recipes={recipes} />
      )}
    </>
  );
};

export default Recipes