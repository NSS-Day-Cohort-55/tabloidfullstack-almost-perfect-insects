import React, { useEffect, useState } from "react";
import { getCategories } from "../modules/categoryManager";

const CategoryList = () => {
  const [categories, setCategories] = useState([])

  const getAllCategories = () => {
    getCategories()
    .then(setCategories)
  }

  useEffect(() => {
    getAllCategories()
  }, [])

  return (
      <div>
          <ul>
              {categories.map((category) => (
                  <li key={category.id}>{category.name}</li>
              ))}
          </ul>
      </div>
  )
}

export default CategoryList;