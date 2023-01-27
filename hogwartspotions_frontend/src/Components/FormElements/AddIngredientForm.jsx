import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import LoadingSpinner from "../UIElements/LoadingSpinner";

const AddIngredientForm = ({ onAdd, ingredients }) => {
  const potionId = useParams().potionId;

  const [chosenIngredient, setChosenIngredient] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const addIngredient = async (e) => {
    e.preventDefault();
    console.log(chosenIngredient);

    try {
      setIsLoading(true);
      const response = await fetch(
        `https://localhost:44390/api/potions/${potionId}/addIngredient`,
        {
          method: "PUT",
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify({
            name: chosenIngredient.name,
          }),
        }
      );
      const responseData = await response.json();
      console.log(responseData);
      setIsLoading(false);

      if (!response.ok) {
        const error = responseData.message;
        console.log(error);
        onAdd(chosenIngredient, false, error); // In AddINgredient the params are: ingredient, responseData, errorMessage
        return error;
      }

      onAdd(chosenIngredient, responseData, false); // if the response is ok, the rsponseData will be the Potion
    } catch (err) {
      setIsLoading(false);
      console.log(err);

      onAdd(chosenIngredient, false, err.message);
    }
  };

  const handleChange = (event) => {
    setChosenIngredient(event.target.value);
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
            Add a new Ingredient
          </Typography>
          <form onSubmit={addIngredient}>
            <FormControl fullWidth>
              <InputLabel id="demo-simple-select-label">Ingredient</InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={chosenIngredient}
                label="Ingredient"
                onChange={handleChange}
              >
                {ingredients.map((i) => (
                  <MenuItem key={i.id} value={i}>
                    {i.name}
                  </MenuItem>
                ))}
              </Select>

              <Button
                type="submit"
                className="form-btn"
                disabled={chosenIngredient === ""}
                variant="contained"
              >
                Add Ingredient
              </Button>
            </FormControl>
          </form>
        </>
      )}
    </>
  );
};

export default AddIngredientForm;
