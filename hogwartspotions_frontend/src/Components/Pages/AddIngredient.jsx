import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

import PotionDetails from "../PotionElements/PotionDetails";
import AddIngredientForm from "../FormElements/AddIngredientForm";
import IngredientWasAdded from "../FormElements/IngredientWasAdded";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";

import "../../App.css";
import SuccessMessage from "../Shared/SuccessMessage";

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


  const clearSuccess = () => {
    setSuccess(null);
    console.log(potion.recipe);
    navigate(`/recipes/${potion.recipe.id}/details`, { replace: true });  
  };
  
  const closeModal = () => {
    setError(null);
    setSuccess(null);
  };
  
  const formatPreviouslyBrewnCount = (count) => { 
    let formattedCount
    switch (count) {
      case 1:
        formattedCount = "first" 
        break;
      case 2:
        formattedCount = "second" 
        break;
      case 3:
        formattedCount = "third" 
        break;
      default:
        formattedCount =`${count}th`
        break;
    }
    return `This is the ${formattedCount} time a Potion of this Recipe has been brewn.`
   }

  const ingredientWasSubmitted = (ingredient, responseData, errorMessage) => {
    !errorMessage && setAddedIngredient(ingredient);
    errorMessage && setError(errorMessage);

    responseData.brewingStatus === "Discovery" &&
      setSuccess(<SuccessMessage />);

    responseData.brewingStatus === "Replica" &&
      setSuccess(`You have successfully brewed the Recipe ${responseData.recipe.name}. ${formatPreviouslyBrewnCount(responseData.recipe.potionsMadeOfRecipe.length)}`);

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
        <MessageModal message={error} onClose={closeModal} onGoTo={clearSuccess} itIsAnError />
      ) : success ? (
        <MessageModal message={success} onClose={closeModal} onGoTo={clearSuccess} buttonText="Go to the Recipe"/>
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
