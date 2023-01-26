import { Button } from "@mui/material";
import Typography from "@mui/material/Typography";

const IngredientWasAdded = ({ onAddAnother }) => {
    const addAnotherIngredient = () => {
      onAddAnother();
    };
  
    return (
      <div>
        <Typography variant="h5" component="div">
          Ingredient has been added successfully.
        </Typography>
        <p>
          <Button variant="contained" onClick={addAnotherIngredient}>
            Add another one
          </Button>
        </p>
      </div>
    );
  };


  export default IngredientWasAdded;