import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

import PotionDetails from "../PotionElements/PotionDetails";
import AddIngredientForm from "../FormElements/AddIngredientForm";
import IngredientWasAdded from "../FormElements/IngredientWasAdded";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";

import "../../App.css";

const AddIngredient = () => {
  const potionId = useParams().potionId;
  const navigate = useNavigate();

  const [addedIngredient, setAddedIngredient] = useState(null);

  const [potion, setPotion] = useState(null);
  const [ingredients, setIngredients] = useState([]);

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const [success, setSuccess] = useState();

  const fetchPotion = async () => {
    const url = `https://localhost:44390/api/potions/details/${potionId}`;
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

      setPotion(responseData);
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  const fetchIngredients = async () => {
    const url = "https://localhost:44390/api/ingredients";

    setIsLoading(true);
    try {
      const response = await fetch(url);
      const responseData = await response.json();
      console.log(responseData);
      setIsLoading(false);

      if (!response.ok) {
        const error = responseData.message;
        setError(error);
        console.log(error);
        return;
      }

      setIngredients(responseData);
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  useEffect(() => {
    fetchPotion();
    fetchIngredients();
  }, []);

  const clearError = () => {
    setError(null);
  };

  const clearSuccess = () => {
    setSuccess(null);
    //navigate("/recipes", { replace: true });    //GO TO RECIPES PAGE FOR NAMING IT
  };

  const ingredientWasSubmitted = (ingredient, responseData, errorMessage) => {
    !errorMessage && setAddedIngredient(ingredient);
    responseData.brewingStatus === "Discovery" &&
      setSuccess("Congratulations! You have invented a new Recipe!");
    responseData.brewingStatus === "Replica" &&
      setSuccess(`You have successfully brewed the Recipe ${responseData.recipe.name}`);
    errorMessage && setError(errorMessage);

    fetchPotion();
  };

  const addAnotherIngredient = () => {
    setAddedIngredient(null);
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
        <MessageModal
          message={success}
          onClear={clearSuccess}
          buttonText="Coooool"
        />
      ) : (
        <div className="add-ingredient-page">
          {potion && <PotionDetails potion={potion} />}

          {addedIngredient ? (
            <IngredientWasAdded onAddAnother={addAnotherIngredient} />
          ) : (
            <AddIngredientForm
              ingredients={ingredients}
              onAdd={ingredientWasSubmitted}
            />
          )}
        </div>
      )}
    </>
  );
};

export default AddIngredient;

/*  Form: 
  input field or drop-down menu for a single ingredient name
  submit button to add ingredients one by one
  
  Help button. Clicking the help button must display five to ten recipes. These recipes must contain the selected ingredients. 
  When the potion does not match any known recipe, a field must be displayed to name it. */
