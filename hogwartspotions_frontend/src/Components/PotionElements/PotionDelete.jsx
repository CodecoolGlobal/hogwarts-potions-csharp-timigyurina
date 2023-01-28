import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";

import Card from "@mui/material/Card";
import Button from "@mui/material/Button";

const PotionDelete = ({potionToDelete, onDelete, onCancelDelete}) => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(false);

  const deletePotion = async (id) => {
    console.log(id);
    const url = await fetch(`https://localhost:44390/api/potions/${id}`, {
      method: "DELETE",
    });
    setIsLoading(true);

    try {
      const response = await fetch(url);

      setIsLoading(false);
      onDelete();

      if (!response.ok) {
        const error = response.message;
        setError(error);
        console.log(error);
        return;
      }
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  const deleteCancelled = () => {
    onCancelDelete()
  };

  return (
    <Card className="delete-potion-dialog" variant="outlined">
      Are you sure you want to delete {potionToDelete.name} (id:{" "}
      {potionToDelete.id})?
      <div className="delete-potion-dialog-buttons">
        <Button onClick={deleteCancelled} variant="outlined" color="primary">
          Cancel
        </Button>
        <Button
          onClick={() => deletePotion(potionToDelete.id)}
          variant="contained"
          color="error"
        >
          Delete
        </Button>
      </div>
    </Card>
  );
};

export default PotionDelete;
