import { useState, useRef } from "react";

import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Button from "@mui/material/Button";

const AddIngredientForm = ({onSubmit, ingredients}) => {
    const [chosenIngredient, setChosenIngredient] = useState("");

    const action = useRef(null);
    const addIngredient = (e) => {
        e.preventDefault();
        //setIsLoading(true);
      };
    
      const handleChange = (event) => {
        setChosenIngredient(event.target.value);
      };

  return (
    <FormControl className="add-ingredient-form" onSubmit={onSubmit}>
      <InputLabel id="demo-simple-select-label">Ingredient</InputLabel>
      <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        value={chosenIngredient}
        label="Ingredient"
        onChange={handleChange}
      >
        {ingredients.map((i) => (
          <MenuItem key={i.id} value={i.id}>
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
  );
};

export default AddIngredientForm;
