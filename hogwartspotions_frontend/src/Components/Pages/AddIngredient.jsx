import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

import Typography from "@mui/material/Typography";
import PotionDetails from "../PotionElements/PotionDetails";
import AddIngredientForm from "../FormElements/AddIngredientForm";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";

import "../../App.css";

const AddIngredient = () => {
  const potionId = useParams().potionId;

  const [potion, setPotion] = useState(null);
  const [ingredients, setIngredients] = useState([]);

  const navigate = useNavigate();

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const [success, setSuccess] = useState();

  const fetchPotion = async () => {
    const url = `https://localhost:44390/api/potions/details/${potionId}`;
    setIsLoading(true);
    
    try {
      const response = await fetch(url);
      const data = await response.json();

      if (!response.ok) {
        const error = response.message;
        setIsLoading(false);
        setError(error);
        console.log(error);
      }
      setPotion(data);
      setIsLoading(false)
      console.log(data);
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
      const data = await response.json();

      if (!response.ok) {
        const error = response.message;
        setIsLoading(false);
        setError(error);
        console.log(error);
      }
      setIngredients(data);
      setIsLoading(false);
      console.log(data);
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
    navigate("/potions", { replace: true });
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
          buttonText="Go to list of Potions"
        />
      ) : (
        <div className="add-ingredient-page">
          {potion && <PotionDetails potion={potion} />}
          <Typography variant="h5" component="div">
            Add new Ingredient to Potion
          </Typography>
          <AddIngredientForm ingredients={ingredients} />
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
