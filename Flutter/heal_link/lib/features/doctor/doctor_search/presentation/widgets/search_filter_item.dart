
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class SearchFilterItem extends StatelessWidget {
  const SearchFilterItem({
    super.key,
    required this.item,
    required TextEditingController searchController,
  }) : _searchController = searchController;

  final String item;
  final TextEditingController _searchController;

  @override
  Widget build(BuildContext context) {
    return ListTile(
      contentPadding: EdgeInsets.symmetric(vertical: 8 , ),
      minLeadingWidth: 10,
      
      leading: CircleAvatar(
        backgroundImage: AssetImage(AppImages.person , 
        
        ),
        radius: 20,
      ),
      title: Text(item , 
      style: AppTextStyles.popins500style14BlackColor,
      ),
      onTap: () {
        _searchController.text = item;
      },
    );
  }
}
