import { Button } from "@mui/material";
import Typography from "@mui/material/Typography";

const IngredientWasAdded = ({ onAddAnother }) => {
    const addAnotherIngredient = () => {
      onAddAnother();
    };
  
    return (
      <div className="added-ingredient">
        <Typography variant="h5" component="div">
          Ingredient has been added successfully.
        </Typography>
        <p>
          <Button variant="contained" color="secondary" onClick={addAnotherIngredient}>
            Add another one
          </Button>
        </p>
      </div>
    );
  };


  export default IngredientWasAdded;