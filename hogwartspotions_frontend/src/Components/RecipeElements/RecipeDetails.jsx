import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";

const RecipeDetails = ({ recipe }) => {
  return (
    <Box sx={{ minWidth: 275 }}>
      <Card variant="outlined" className="recipe-details-card">
        <CardContent sx={{ display: "flex", flexDirection: "column", gap: "2em" }}>
          <div>
            <Typography variant="h5" component="div">
              {recipe.name}
            </Typography>
            <Typography sx={{ mb: 1.5 }} color="text.secondary">
              {recipe.brewingStatus}
            </Typography>
          </div>
          <div>
            <Typography variant="h6" component="h6">
              Creator's details
            </Typography>
            <Typography variant="body">Name: {recipe.student.name}</Typography>
            <Typography variant="body">
              House: {recipe.student.houseType}
            </Typography>
            <Typography variant="body">
              Pet: {recipe.student.petType}
            </Typography>
          </div>
          <div>
            <Typography variant="h6" component="h6">
              Ingredients in this Recipe
            </Typography>
            <Typography variant="body">
              {recipe.consistencies.map((c) => (
                <li key={c.recipeId + c.ingredientId}>{c.ingredient.name}</li>
              ))}
            </Typography>
          </div>
          <div>
          <Typography variant="h6" component="h6">
              Potions made of this Recipe
            </Typography>
            <Typography variant="body">

            <p>Count: {recipe.potionsMadeOfRecipe.length}</p>
            {recipe.potionsMadeOfRecipe.map((p) => (
              <div key={p.id}>
                ~ {p.name} - {p.brewingStatus}
              </div>
            ))}
            </Typography>
          </div>
        </CardContent>
      </Card>
    </Box>
  );
};

export default RecipeDetails;
