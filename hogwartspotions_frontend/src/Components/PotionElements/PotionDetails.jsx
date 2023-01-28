import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";

const PotionDetails = ({ potion }) => {
  return (
    <Box sx={{ minWidth: 275 }}>
      <Card variant="outlined" className="potion-details-card">
        <CardContent
          sx={{ display: "flex", flexDirection: "column", gap: "2em" }}
        >
          <div>
            <Typography variant="h5" component="div">
              {potion.name}
            </Typography>
            <Typography sx={{ mb: 1.5 }} color="text.secondary">
              {potion.brewingStatus}
            </Typography>
          </div>
          <div>
            <Typography variant="h6" component="h6">
              Recipe details
            </Typography>
            <Typography variant="body">
              Name: {potion.recipe ? potion.recipe.name : "no recipe"}
            </Typography>
          </div>
          <div>
            <Typography variant="h6" component="h6">
              Student details
            </Typography>
            <Typography variant="body">Name: {potion.student.name}</Typography>
            <Typography variant="body">
              House: {potion.student.houseType}
            </Typography>
            <Typography variant="body">
              Pet: {potion.student.petType}
            </Typography>
          </div>
          <div>
            <Typography variant="h6" component="h6">
              Ingredient details
            </Typography>
            <Typography variant="body">
              {potion.potionIngredients.length > 0
                ? potion.potionIngredients.map((i) => (
                    <li key={i.ingredient.id}>{i.ingredient.name}</li>
                  ))
                : "No ingredient has been added yet"}
            </Typography>
          </div>
        </CardContent>
      </Card>
    </Box>
  );
};

export default PotionDetails;
