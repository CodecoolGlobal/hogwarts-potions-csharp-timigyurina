import Card from "@mui/material/Card";
import Button from "@mui/material/Button";

const PotionDelete = ({
  potionToDelete,
  onDelete,
  onCancelDelete,
  onDeletionStart,
  onDeletionEnd,
  onDeletionError,
}) => {
  const deletePotion = async (id) => {
    console.log(id);
    onDeletionStart(); // this prop is for informing the Potions component that the loading state needs to be set true

    try {
      const url = `https://localhost:44390/api/potions/${id}`;
      const response = await fetch(url, {
        method: "DELETE",
      });

      const responseData = await response.json();
      console.log(responseData);
      onDeletionEnd(); // informing the Potions component that the loading state needs to be set false
      onDelete(); // informing the Potions component to fetch the Potions again

      if (!response.ok) {
        const error = responseData.message;
        onDeletionError(error);
        return;
      }
    } catch (err) {
      onDeletionEnd();
      onDeletionError(err.message); // informing the Potions component to set the error state
      console.log(err);
    }
  };

  const deleteCancelled = () => {
    onCancelDelete();
  };

  return (
    <Card className="delete-potion-dialog" variant="outlined">
      Are you sure you want to delete {potionToDelete.name} (id:
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
