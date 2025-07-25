import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';
import '../../../../../../core/widgets/custom_full_button.dart';
import '../../../../../../core/widgets/custom_text_form_field2.dart';
import '../../../../../../generated/l10n.dart';

class AddPrescriptionBodyBottom extends StatelessWidget {
  const AddPrescriptionBodyBottom({
    super.key,
    required this.frequencyController,
    required this.formKey,
    required this.medicineNameController,
    required this.instructionsController,
    required this.onAddPressed,
  });

  final TextEditingController frequencyController;
  final TextEditingController medicineNameController;
  final TextEditingController instructionsController;
  final GlobalKey<FormState> formKey;
  final void Function() onAddPressed;

  @override
  Widget build(BuildContext context) {
    return Column(
      spacing: 8,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: [
            Text.rich(
              TextSpan(
                children: [
                  TextSpan(
                    text: S.of(context).medicine_name,
                    style: AppTextStyles.popins500style12BlackColor,
                  ),
                  TextSpan(
                    text: ':',
                    style: AppTextStyles.popins500style12BlackColor.copyWith(
                      fontSize: 14,
                    ),
                  ),
                ],
              ),
            ),
            Spacer(),
            Text(
              S.of(context).frequency_of_use,
              style: AppTextStyles.popins400style12LightBlackColor.copyWith(
                color: AppColors.kBlackColor,
              ),
            ),
          ],
        ),
        Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          spacing: 8,
          children: [
            Expanded(
              child: CustomTextFormField2(
                hintText: '',
                keyboardType: TextInputType.text,
                controller: medicineNameController,
                validator: (value) {
                  if (value.toString().isEmpty) {
                    return 'enter medicine name';
                  }
                  return null;
                },
                borderRadiusSize: 8,
                fillColor: Colors.transparent,
                borderSideColor: Color(0xffC4C4C4),
              ),
            ),
            Stack(
              children: [
                SizedBox(
                  width: 107,
                  height: 40,
                  child: CustomTextFormField2(
                    hintText: '',
                    keyboardType: TextInputType.number,
                    controller: frequencyController,
                    validator: (value) {
                      if (value.toString().isEmpty) {
                        return 'enter frequency';
                      }
                      return null;
                    },
                    borderRadiusSize: 8,
                    fillColor: Colors.transparent,
                    borderSideColor: Color(0xffC4C4C4),
                  ),
                ),
                Positioned(
                  top: 2,
                  right: 14,
                  child: InkWell(
                    onTap: () {
                      int current = int.tryParse(frequencyController.text) ?? 1;
                      frequencyController.text = (current + 1).toString();
                    },
                    child: Transform.rotate(
                      angle: -1.57,
                      child: SvgPicture.asset(
                        AppImages.arrow,
                        colorFilter: ColorFilter.mode(
                          Color(0xff6D6B6B),
                          BlendMode.srcIn,
                        ),
                      ),
                    ),
                  ),
                ),
                Positioned(
                  bottom: 2,
                  right: 14,
                  child: InkWell(
                    onTap: () {
                      int current = int.tryParse(frequencyController.text) ?? 1;
                      if (current > 1) {
                        frequencyController.text = (current - 1).toString();
                      }
                    },
                    child: Transform.rotate(
                      angle: 1.57,
                      child: SvgPicture.asset(
                        AppImages.arrow,
                        colorFilter: ColorFilter.mode(
                          Color(0xff6D6B6B),
                          BlendMode.srcIn,
                        ),
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ],
        ),
        Text.rich(
          TextSpan(
            children: [
              TextSpan(
                text: S.of(context).usage_instructions,
                style: AppTextStyles.popins500style12BlackColor,
              ),
              TextSpan(
                text: ':',
                style: AppTextStyles.popins500style12BlackColor.copyWith(
                  fontSize: 14,
                ),
              ),
            ],
          ),
        ),
        CustomTextFormField2(
          hintText: '',
          keyboardType: TextInputType.text,
          controller: instructionsController,
          validator: (value) {
            return null;
          },
          borderRadiusSize: 8,
          fillColor: Colors.transparent,
          borderSideColor: Color(0xffC4C4C4),
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            SizedBox(
              width: 88,
              height: 34,
              child: CustomFullButton(
                text: S.of(context).add,
                onPressed: onAddPressed,
                radios: 8,
                textStyle: AppTextStyles.popins400style12LightBlackColor
                    .copyWith(color: Colors.white),
              ),
            ),
          ],
        ),
      ],
    );
  }
}
