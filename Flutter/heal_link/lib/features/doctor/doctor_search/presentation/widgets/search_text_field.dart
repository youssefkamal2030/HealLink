
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_text_form_field2.dart';
import 'package:heal_link/generated/l10n.dart';

// class SearhTextField extends StatefulWidget {
//   const SearhTextField({
//     super.key,
//     required this.searchController,
//     required this.focusNode,
//     this.searchOnChanged,
//   });
//   final TextEditingController searchController;
//   final FocusNode focusNode;
//   final void Function(dynamic)? searchOnChanged;
//   @override
//   State<SearhTextField> createState() => _SearhTextFieldState();
// }

// class _SearhTextFieldState extends State<SearhTextField> {
//   @override
//   Widget build(BuildContext context) {
//     return CustomTextFormField2(
//       borderSideColor:
//           widget.focusNode.hasFocus
//               ? AppColors.kPrimaryColor
//               : Colors.transparent,
//       hintText: S.of(context).searchPatient,
//       keyboardType: TextInputType.text,
//       controller: widget.searchController,
//       focusNode: widget.focusNode,
//       validator: (value) {
//         return null;
//       },
//       onChanged: widget.searchOnChanged,
//       suffixIconResponse: () {
//         setState(() {
//           widget.searchController.clear();
//           widget.focusNode.unfocus();
//         });
//       },
//       suffix:
//           widget.focusNode.hasFocus ? SvgPicture.asset(AppImages.xIcon) : null,
//       prefixIcon: AppImages.search,
//       borderRadiusSize: 8,
//     );
//   }
// }





class SearhTextField extends StatefulWidget {
  const SearhTextField({
    super.key,
    required this.searchController,
    required this.focusNode,
    this.searchOnChanged,
  });
  final TextEditingController searchController;
  final FocusNode focusNode;
  final void Function(dynamic)? searchOnChanged;
  @override
  State<SearhTextField> createState() => _SearhTextFieldState();
}

class _SearhTextFieldState extends State<SearhTextField> {
  @override
  Widget build(BuildContext context) {
    return CustomTextFormField2(
      borderSideColor:
          widget.focusNode.hasFocus
              ? AppColors.kPrimaryColor
              : Colors.transparent,
      hintText: S.of(context).searchPatient,
      keyboardType: TextInputType.text,
      controller: widget.searchController,
      focusNode: widget.focusNode,
      validator: (value) {
        return null;
      },
      onChanged: widget.searchOnChanged,
      // onSaved: (value) {},
      // onTap: () {},
      suffixIconResponse: () {
        setState(() {
          widget.searchController.clear();
          widget.focusNode.unfocus();
        });
      },
      suffix:
          widget.focusNode.hasFocus ? SvgPicture.asset(AppImages.xIcon) : null,
      prefixIcon: AppImages.search,
      borderRadiusSize: 8,
    );
  }
}
