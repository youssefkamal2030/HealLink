import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class SubscribtionMonthlyDropdown extends StatefulWidget {
  const SubscribtionMonthlyDropdown({super.key});

  @override
  State<SubscribtionMonthlyDropdown> createState() =>
      _SubscribtionMonthlyDropdownState();
}

class _SubscribtionMonthlyDropdownState
    extends State<SubscribtionMonthlyDropdown> {
  String selectedValue = 'Monthly';

  final List<String> options = ['Daily', 'Weekly', 'Monthly', 'Yearly'];

  @override
  Widget build(BuildContext context) {
    return DropdownButton<String>(
      value: selectedValue,
      underline: const SizedBox(), // removes the default underline
      icon: const Icon(
        Icons.keyboard_arrow_down_rounded,
        color: AppColors.kDarkGreyColor,
        size: 22,
      ),
      dropdownColor: Colors.white,
      style: AppTextStyles.popins500style14BlackColor.copyWith(
        color: AppColors.kDarkGreyColor,
      ),
      items:
          options.map((String value) {
            return DropdownMenuItem<String>(value: value, child: Text(value));
          }).toList(),
      onChanged: (newValue) {
        setState(() {
          selectedValue = newValue!;
        });
      },
    );
  }
}
