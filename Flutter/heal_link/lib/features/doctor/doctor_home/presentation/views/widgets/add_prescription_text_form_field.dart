import 'package:flutter/material.dart';

class AddPrescriptionTextFormField extends StatelessWidget {
  const AddPrescriptionTextFormField({
    super.key,
    required this.hintText,
    required this.textEditingController,
    required this.textInputType,
    required this.validator,
  });

  final String hintText;
  final TextEditingController textEditingController;
  final TextInputType textInputType;
  final String? Function(String?) validator;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: textEditingController,
      keyboardType: textInputType,
      style: const TextStyle(
        color: Color(0xFF928F8F),
        fontSize: 16,
        fontFamily: 'Tajawal',
        fontWeight: FontWeight.w400,
      ),
      validator: validator,
      decoration: InputDecoration(
        hintText: hintText,
        hintStyle: TextStyle(
          color: Color(0x7F928F8F),
          fontSize: 16,
          fontFamily: 'Tajawal',
          overflow: TextOverflow.clip,
          fontWeight: FontWeight.w400,
        ),
        border: InputBorder.none,
        isCollapsed: true,
        contentPadding: EdgeInsets.only(left: 5, top: 4),
      ),
      cursorColor: Color(0xFF928F8F),
    );
  }
}
