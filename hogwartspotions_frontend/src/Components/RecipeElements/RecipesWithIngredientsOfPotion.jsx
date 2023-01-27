import { useState, useEffect } from "react";

import RecipesTable from "../RecipeElements/RecipesTable";
import LoadingSpinner from "../UIElements/LoadingSpinner";

const RecipesWithIngredientsOfPotion = ({ potionId }) => {
  const [recipes, setRecipes] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();

  const fetchRecipesWithIngredients = async () => {
    const url = `https://localhost:44390/api/potions/${potionId}/help`;

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
    fetchRecipesWithIngredients();
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
        error
      ) : (
        <RecipesTable recipes={recipes} />
      )}
    </>
  );
};

export default RecipesWithIngredientsOfPotion;
