import { useState } from "react";
import { useParams } from "react-router-dom";

import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import ModalBase from "./ModalBase";
import RecipesWithIngredientsOfPotion from "../RecipeElements/RecipesWithIngredientsOfPotion";

const style = {
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  gap: "15px",
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 800,
  bgcolor: "background.paper",
  border: "2px solid black",
  boxShadow: 24,
  p: 4,
};

const PotionRecipeHelpModal = ({
  onClose,
  buttonText = "Close",
}) => {
  const potionId = useParams().potionId;
  const [open, setOpen] = useState(true);

  const handleClose = () => {
    onClose();
    setOpen(false);
  };

  return (
    <ModalBase open={open} handleClose={handleClose} boxStyle={style}>
      <Typography id="transition-modal-title" variant="h5" component="h2">
        List of Recipes that contain this Potion's Ingredients
      </Typography>
      <RecipesWithIngredientsOfPotion potionId={potionId} />
      <div className="modal-buttons-container">
        <Button onClick={handleClose} variant="outlined" color="success">
          {buttonText}
        </Button>
      </div>
    </ModalBase>
  );
};

export default PotionRecipeHelpModal;
