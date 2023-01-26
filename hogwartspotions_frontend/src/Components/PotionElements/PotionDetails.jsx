import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";

const PotionDetails = ({ potion }) => {
  return (
    <Box sx={{ minWidth: 275 }}>
      <Card variant="outlined">
        <CardContent>
          <Typography variant="h5" component="div">
            {potion.name}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            {potion.brewingStatus}
          </Typography>
          <div>
            <h3>Recipe details:</h3>
            <p>Name: {potion.recipe ? potion.recipe.name : "no recipe"}</p>
          </div>
          <div>
            <h3>Student details:</h3>
            <p>Name: {potion.student.name}</p>
            <p>House: {potion.student.houseType}</p>
            <p>Pet: {potion.student.petType}</p>
          </div>
          <div>
            <h3>Ingredients:</h3>
            {potion.potionIngredients.length > 0
              ? potion.potionIngredients.map((i) => (
                  <li key={i.ingredient.id}>{i.ingredient.name}</li>
                ))
              : "No ingredient has been added yet"}
          </div>
        </CardContent>
      </Card>
    </Box>
  );
};

export default PotionDetails;
