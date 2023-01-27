import { useState } from "react";
import { useParams } from "react-router-dom";

import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import HelpCenterIcon from "@mui/icons-material/HelpCenter";

import LoadingSpinner from "../UIElements/LoadingSpinner";
import PotionRecipeHelpModal from "../UIElements/PotionRecipeHelpModal";

const AddIngredientForm = ({ onAdd, ingredients }) => {
  const potionId = useParams().potionId;

  const [chosenIngredient, setChosenIngredient] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [helpModalOpen, setHelpModalOpen] = useState(false);

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

  const openHelpModal = () => {
    setHelpModalOpen(true);
  };

  const closeHelpModal = () => {
    setHelpModalOpen(false);
  };

  return (
    <>
      {isLoading ? (
        <div className="center">
          <LoadingSpinner asOverlay />
        </div>
      ) : (

        <>
          {helpModalOpen && <PotionRecipeHelpModal onClose={closeHelpModal} />}

          <div className="add-ingredient-form">
            <Typography variant="h5" component="div">
              Add a new Ingredient
              <Tooltip title="Help" placement="right-start" followCursor={true}>
                <IconButton onClick={openHelpModal}>
                  <HelpCenterIcon />
                </IconButton>
              </Tooltip>
            </Typography>
            <form onSubmit={addIngredient} className="add-ingredient-form">
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">
                  Ingredient
                </InputLabel>
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
                  sx={{ margin: "1em" }}
                  disabled={chosenIngredient === ""}
                  variant="contained"
                  color="success"
                >
                  Add Ingredient
                </Button>
              </FormControl>
            </form>
          </div>
        </>
      )}
    </>
  );
};

export default AddIngredientForm;
