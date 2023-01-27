import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";

const RecipeDetails = ({ recipe }) => {
  return (
    <Box sx={{ minWidth: 275 }}>
      <Card variant="outlined">
        <CardContent>
          <Typography variant="h5" component="div">
            {recipe.name}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            {recipe.brewingStatus}
          </Typography>
          <div>
            <h3>Creator's details:</h3>
            <p>Name: {recipe.student.name}</p>
            <p>House: {recipe.student.houseType}</p>
            <p>Pet: {recipe.student.petType}</p>
          </div>
          <div>
            <h3>Ingredients in this Recipe:</h3>
            {recipe.consistencies.map((r) => (
              <li key={r.ingredient.id}>{r.ingredient.name}</li>
            ))}
          </div>
          <div>
            <h3>Potions made of this Recipe:</h3>
            <p>Count: {recipe.potionsMadeOfRecipe.length}</p>
            {recipe.potionsMadeOfRecipe.map((p) => (
              <>
                <div key={p.id}>
                  ~  {p.name} - {p.brewingStatus}
                </div>
              </>
            ))}
          </div>
        </CardContent>
      </Card>
    </Box>
  );
};

export default RecipeDetails;
